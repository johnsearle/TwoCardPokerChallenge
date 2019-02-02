import { Component, OnInit } from '@angular/core';
import { PokerGame, Round, Card, OverallResult, AppendRoundRequest, RankRoundRequest, DealCardsRequest, ShuffleDeckRequest, NewGameRequest, OverallResultRequest } from '../model';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-dealer',
  templateUrl: './dealer.component.html',
  styleUrls: ['./dealer.component.css']
})
export class DealerComponent implements OnInit {
  
  minRounds: number = 2;
  maxRounds: number = 5;
  numPlayers: number = 2;
  pokerGame: PokerGame;
  overallResults: OverallResult[];

  constructor(private gameService: GameService) { }

  ngOnInit() {
  }

  newGame(): void {
    const request = <NewGameRequest>({numPlayers: this.numPlayers});
    this.gameService.getNewGame(request)
        .subscribe(result => this.setPokerGameResponse(result));
  }

  newRound(): void {
    const request = <AppendRoundRequest>({existingRounds: this.pokerGame.rounds});
    this.gameService.getNewRound(request)
        .subscribe(result => this.setRoundResponse(result));
  }

  shuffle(): void {
    const request = <ShuffleDeckRequest>({deck: this.pokerGame.deck});
    this.gameService.shuffle(request)
        .subscribe(result => this.setShuffleResponse(result));
  }

  deal(): void {
    const request = <DealCardsRequest>({pokerGame: this.pokerGame});  
    this.gameService.deal(request)
        .subscribe(result => this.setPokerGameResponse(result));
  }

  rank(index: number): void {  
    const request = <RankRoundRequest>({round: this.pokerGame.rounds[index]});
    this.gameService.rank(request)
        .subscribe(result => this.setRankResponse(index, result));
  }

  finish(): void {
    const request = <OverallResultRequest>({pokerGame: this.pokerGame});
    this.gameService.getOverallResults(request)
        .subscribe(result => this.overallResults = result);
  } 
  
  startNew(): void {  
    this.pokerGame = null;
    this.overallResults = null;
  }

  setPokerGameResponse(result: PokerGame): void {
    this.pokerGame = result;     
  }

  setRoundResponse(result: Round[]): void {
    this.pokerGame.rounds = result;    
  }

  setShuffleResponse(result: Card[]): void {
    this.pokerGame.deck = result;
    let currentRound = this.pokerGame.rounds[this.pokerGame.rounds.length - 1];  
    currentRound.shuffled = true;
  }

  setRankResponse(index: number, result: Round): void {
    this.pokerGame.rounds[index] = result;  
  }

  canAddRound(): boolean { 
    if (this.pokerGame) {
        let roundCount = this.pokerGame.rounds.length;
        let currentRound = this.pokerGame.rounds[roundCount - 1];   
        return currentRound.players[0].hand &&
          currentRound.players[0].outcome !== 'Unknown' &&
          roundCount < this.maxRounds &&
          !this.overallResults;
    }
    return false;
  }

  canShuffle(index: number): boolean {
    if (this.pokerGame) {
        let currentRound = this.pokerGame.rounds[index];   
        return !currentRound.players[0].hand;
    }
    return false;
  }

  canDeal(index: number): boolean {  
    if (this.pokerGame) {
        let currentRound = this.pokerGame.rounds[index];   
        return currentRound.shuffled;
    }
    return false;
  }

  canRank(index: number): boolean { 
    if (this.pokerGame) {
        let currentRound = this.pokerGame.rounds[index];   
        return currentRound.players[0].hand && (currentRound.players[0].outcome === 'Unknown');
    }
    return false;
  }

  canFinish(): boolean { 
    if (this.pokerGame) {
        let roundCount = this.pokerGame.rounds.length;
        let currentRound = this.pokerGame.rounds[roundCount - 1];
        return currentRound.players[0].hand &&
          currentRound.players[0].outcome !== 'Unknown' &&
          roundCount >= this.minRounds &&
          !this.overallResults;
    }
    return false;
  }  

  canStartNewGame(): boolean {  
    return this.overallResults && this.overallResults.length > 0;
  }

}
