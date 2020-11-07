import { Component, Input } from '@angular/core';
import { Player } from './components/models/player';
import { Game } from './enums/game.enum';
import { PlayerType } from './enums/player-type.enum';

@Component({
  selector: 'sh-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss'],
})
export class GameBoardComponent {
  @Input() set game(value: Game) {
    switch (value) {
      case Game.ComputerVsComputer:
        this.player1 = new Player('Computer 1', PlayerType.Computer);
        this.player2 = new Player('Computer 2', PlayerType.Computer);
        break;
      case Game.ComputerVsHuman:
        this.player1 = new Player('Computer 1', PlayerType.Computer);
        this.player2 = new Player('Human 1', PlayerType.Human);
        break;
      case Game.HumanVsHuman:
        this.player1 = new Player('Human 1', PlayerType.Human);
        this.player2 = new Player('Human 2', PlayerType.Human);
        break;

      default:
        break;
    }
  }

  player1: Player;
  player2: Player;

  constructor() {}
}
