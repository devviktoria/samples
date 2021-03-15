import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Joke } from '../models/joke';

@Injectable({
  providedIn: 'root',
})
export class JokeService {
  private jokeBaseUrl = 'http://localhost:5000/api/Joke';
  private latestJokesUrl = this.jokeBaseUrl + '/GetLatestJokes';
  private mostPopularJokesUrl = this.jokeBaseUrl + '/GetMostPopularJokes';
  private incrementEmotionCounterUrl =
    this.jokeBaseUrl + '/IncrementEmotionCounter';
  private userJokeUrl = this.jokeBaseUrl + '/GetUserJokes';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private http: HttpClient) {}

  getJoke(jokeId: string): Observable<Joke> {
    let url: string = `${this.jokeBaseUrl}/${jokeId}`;
    return this.http.get<Joke>(url, this.httpOptions).pipe(
      tap((_) => true),
      catchError(this.handleError<Joke>('getJoke'))
    );
  }

  getLatestJokes(): Observable<Joke[]> {
    return this.http.get<Joke[]>(this.latestJokesUrl, this.httpOptions).pipe(
      tap((_) => true),
      catchError(this.handleError<Joke[]>('getLatestJokes', []))
    );
  }

  getMostPopularJokes(): Observable<Joke[]> {
    return this.http
      .get<Joke[]>(this.mostPopularJokesUrl, this.httpOptions)
      .pipe(
        tap((_) => true),
        catchError(this.handleError<Joke[]>('getMostPopularJokesUrl', []))
      );
  }

  getUserJokes(
    mode: string,
    userEmail: string,
    jokesPerPage: number,
    pageIndex: number
  ): Observable<Joke[]> {
    let url: string = `${this.userJokeUrl}/${mode}/${userEmail}/${jokesPerPage}/${pageIndex}`;
    return this.http.get<Joke[]>(url, this.httpOptions).pipe(
      tap((_) => true),
      catchError(this.handleError<Joke[]>('getUserJokes', []))
    );
  }

  incrementEmotionCounter(jokeId: string, emotion: string): Observable<Joke> {
    let url: string = `${this.incrementEmotionCounterUrl}/${jokeId}/${emotion}`;
    return this.http.put<Joke>(url, null, this.httpOptions).pipe(
      tap((_) => true),
      catchError(this.handleError<Joke>('incrementEmotionCounter'))
    );
  }

  addJoke(joke: Joke): Observable<Joke> {
    return this.http.post<Joke>(this.jokeBaseUrl, joke, this.httpOptions).pipe(
      tap((_) => true),
      catchError(this.handleError<Joke>('addJoke'))
    );
  }

  updateJoke(joke: Joke): Observable<any> {
    let url: string = `${this.jokeBaseUrl}/${joke.Id}`;
    return this.http.put<Joke>(url, joke, this.httpOptions).pipe(
      tap((_) => true),
      catchError(this.handleError<any>('updateJoke'))
    );
  }

  deleteJoke(jokeId: string): Observable<any> {
    let url: string = `${this.jokeBaseUrl}/${jokeId}`;
    return this.http.delete(url, this.httpOptions).pipe(
      tap((_) => true),
      catchError(this.handleError<any>('deleteJoke'))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
