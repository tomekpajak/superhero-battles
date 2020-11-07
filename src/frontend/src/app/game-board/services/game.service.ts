import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Player } from '../components/models/player';
import { SuperHero } from '../components/models/super-hero';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private playersCount: number = 2;
  private playersCards: [Player, SuperHero][] = new Array<[Player, SuperHero]>();

  private winnerSource = new BehaviorSubject<Player>(null);
  winner$ = this.winnerSource.asObservable();

  private isTurnFinishedSource = new BehaviorSubject<boolean>(false);
  isTurnFinished$ = this.isTurnFinishedSource.asObservable();

  constructor() { }

  choseCard(player: Player, card: SuperHero) {
    this.playersCards.push([player, card]);
    if (this.playersCards.length == this.playersCount) {
      this.checkWinner(this.playersCards[0], this.playersCards[1]);
      this.playersCards.length = 0;
    }
  }

  private checkWinner(player1: [player: Player, card: SuperHero],
                      player2: [player: Player, card: SuperHero]) {
    let winner;
    
    let player1Card = player1[1];
    let player2Card = player2[1];
    
    if (player1Card.attack > player2Card.attack) {
      winner = player1[0];
    } else {
      winner = player2[0];
    }

    this.sendWinner(winner);
  }

  private sendWinner(player: Player) {
    this.winnerSource.next(player);
    this.isTurnFinishedSource.next(true);
    this.isTurnFinishedSource.next(false);
  }
}
