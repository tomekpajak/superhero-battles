import { Component, Input, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SuperHeroApiService } from '../../../core/api/super-hero-api.service';
import { PlayerType } from '../../enums/player-type.enum';
import { GameService } from '../../services/game.service';
import { Player } from '../models/player';
import { SuperHero } from '../models/super-hero';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ISuperHeroResponse } from '../../interfaces/super-hero-response';

@Component({
  selector: 'sh-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.scss'],
})
export class PlayerComponent implements OnInit {
  @Input() player: Player;

  card: SuperHero;
  wins: number = 0;
  isWinner: boolean = false;
  isVisible: boolean = false;
  isDisabledActions: boolean = false;

  playerTypeEnum = PlayerType;

  private winnerSubscription: Subscription;

  constructor(
    private gameService: GameService,
    private superHeroApiService: SuperHeroApiService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.winnerSubscription = this.gameService.winner$.subscribe((winner) => {
      this.isWinner = winner && winner.name == this.player.name ? true : false;
      if (this.isWinner) this.wins++;
    });
  }

  ngOnDestroy(): void {
    this.winnerSubscription.unsubscribe();
  }

  playTurn() {
    this.reset();
    this.isVisible = true;

    switch (this.player.type) {
      case PlayerType.Computer:
        this.getRandomCard();
        break;

      default:
        break;
    }
  }

  getRandomCard() {
    this.superHeroApiService
      .getSuperHeroRandom()
      .subscribe(
        (response: ISuperHeroResponse) => {
          if (response.succeeded) {
            this.card = response.data;
            this.gameService.choseCard(this.player, this.card);  
          } else {
            this.snackBar.open(response.message, null, {
              duration: 3000
            });  

            response.errors.forEach(e => {
              this.snackBar.open(e, null, {
                duration: 3000
              });  
            })
          }
        },
        (error) => {   
          this.snackBar.open(error.message, null, {
            duration: 3000
          });
        });

    this.isDisabledActions = true;
  }

  getChooseCard() {}

  private reset() {
    this.isWinner = false;
    this.isVisible = false;
    this.isDisabledActions = false;
    this.card = null;
  }
}
