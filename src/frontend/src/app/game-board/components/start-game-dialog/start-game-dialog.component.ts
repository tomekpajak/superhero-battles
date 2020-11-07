import { Component } from '@angular/core';
import { MatDialogRef } from "@angular/material/dialog";
import { Game } from '../../enums/game.enum';

@Component({
  selector: 'sh-start-game-dialog',
  templateUrl: './start-game-dialog.component.html',
  styleUrls: ['./start-game-dialog.component.scss']
})
export class StartGameDialogComponent {

  constructor(private dialogRef: MatDialogRef<StartGameDialogComponent, Game>) { }

  onSelectCvsC() {
    this.dialogRef.close(Game.ComputerVsComputer);
  }

  onSelectCvsH() {
    this.dialogRef.close(Game.ComputerVsHuman);
  }
}
