import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ISuperHeroResponse } from 'src/app/game-board/interfaces/super-hero-response';

@Injectable({
  providedIn: 'root'
})
export class SuperHeroApiService {
  readonly baseUrl = "/api";
  readonly superHeroUrl = "/v1.0/superheroes/random";
  
  constructor(private httpClient: HttpClient) { }

  getSuperHeroRandom() {
    return this.httpClient.get<ISuperHeroResponse>(this.baseUrl + this.superHeroUrl);
  }
}
