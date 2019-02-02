export class PokerGame {
  deck: Card[];
  rounds: Round[];
}

export class Card {
  suit: string;
  rank: string;
}

export class Round {
  players: Player[]; 
  shuffled: boolean;
}

export class Player {
      id: number;
      hand: Card[];
      outcome: string;
      score: number;
}

export class OverallResult {
  playerId: number; 
  overallScore: number;
}

export class AppendRoundRequest {
  existingRounds: Round[];
}

export class RankRoundRequest {
  round: Round;
}

export class DealCardsRequest {
  pokerGame: PokerGame;
}

export class ShuffleDeckRequest {
  deck: Card[];
}

export class NewGameRequest {
  numPlayers: number;
}

export class OverallResultRequest {
  pokerGame: PokerGame;
}
