import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { PokerGame, Round, Card, OverallResult, AppendRoundRequest, RankRoundRequest, DealCardsRequest, ShuffleDeckRequest, NewGameRequest, OverallResultRequest } from '../model';
import { MessageService } from './message.service';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })
export class GameService {  

  private baseUrl = environment.apiUrl; 

  constructor(private http: HttpClient, private messageService: MessageService) { }

  getNewGame (request: NewGameRequest): Observable<PokerGame> {

    const url = `${this.baseUrl}/newGame`;

    return this.http.post<NewGameRequest>(url, request, httpOptions).pipe(
      tap(_ => this.log(`New game.`)),
      catchError(this.handleError<any>('getNewGame'))
    );   
  }

  getNewRound (request: AppendRoundRequest): Observable<Round[]> {

    const url = `${this.baseUrl}/newRound`;    

    return this.http.post<AppendRoundRequest>(url, request, httpOptions).pipe(
      tap(_ => this.log(`Append round.`)),
      catchError(this.handleError<any>('getNewRound'))
    );
  }

  shuffle (request: ShuffleDeckRequest): Observable<Card[]> {

    const url = `${this.baseUrl}/shuffle`; 

    return this.http.post<ShuffleDeckRequest>(url, request, httpOptions).pipe(
      tap(_ => this.log(`Shuffled.`)),
      catchError(this.handleError<any>('shuffle'))
    );
  }

  deal (request: DealCardsRequest): Observable<PokerGame> {

    const url = `${this.baseUrl}/deal`; 

    return this.http.post<DealCardsRequest>(url, request, httpOptions).pipe(
      tap(_ => this.log(`Dealt.`)),
      catchError(this.handleError<any>('deal'))
    );
  }

  getOverallResults (request: OverallResultRequest): Observable<OverallResult[]> {

    const url = `${this.baseUrl}/results`; 

    return this.http.post<OverallResultRequest>(url, request, httpOptions).pipe(
      tap(_ => this.log(`Get overall results.`)),
      catchError(this.handleError<any>('getOverallResults'))
    );
  }

  rank (request: RankRoundRequest): Observable<Round> {

    const url = `${this.baseUrl}/rank`; 

    return this.http.post<RankRoundRequest>(url, request, httpOptions).pipe(
      tap(_ => this.log(`Ranked.`)),
      catchError(this.handleError<any>('rank'))
    );
  }

  private handleError<T>
   (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
 
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead
 
      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);
 
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }


private log(message: string) {
    this.messageService.add(`GameService: ${message}`);
  }

}


