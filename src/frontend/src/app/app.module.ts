import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ControlPanelComponent } from './game-board/components/control-panel/control-panel.component';
import { PlayerComponent } from './game-board/components/player/player.component';
import { StartGameDialogComponent } from './game-board/components/start-game-dialog/start-game-dialog.component';
import { GameBoardComponent } from './game-board/game-board.component';
import { MaterialModule } from './shared/modules/material.module';

@NgModule({
  declarations: [
    AppComponent,
    GameBoardComponent,
    PlayerComponent,
    ControlPanelComponent,
    AppComponent,
    StartGameDialogComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    MaterialModule,
    FlexLayoutModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [StartGameDialogComponent]
})
export class AppModule { }
