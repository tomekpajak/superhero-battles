import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { StartGameDialogComponent } from './game-board/components/start-game-dialog/start-game-dialog.component';
import { Game } from './game-board/enums/game.enum';

@Component({
  selector: 'sh-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'superhero battles';
  gameType: Game;

  constructor(private dialog: MatDialog) {}

  ngOnInit(): void {
    this.openDialog();
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    const dialogRef = this.dialog.open<StartGameDialogComponent, any, Game>(StartGameDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
        game => this.gameType = game
    );     
  }
}
