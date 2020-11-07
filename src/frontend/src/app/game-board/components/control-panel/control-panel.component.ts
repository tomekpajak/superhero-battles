import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { Game } from '../../enums/game.enum';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'sh-control-panel',
  templateUrl: './control-panel.component.html',
  styleUrls: ['./control-panel.component.scss'],
})
export class ControlPanelComponent implements OnInit {
  @Input() game: Game;
  @Output() playTurnEvent = new EventEmitter();

  private isTurnFinishedSubscription: Subscription;
  buttonLabel: string = 'Play !';
  buttonDisabled: boolean = false;

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.isTurnFinishedSubscription = this.gameService.isTurnFinished$.subscribe(
      () => {
        this.buttonLabel = 'Play !';
        this.buttonDisabled = false;
      }
    );
  }

  ngOnDestroy() {
    this.isTurnFinishedSubscription.unsubscribe();
  }

  onPlay() {
    this.playTurnEvent.emit();

    this.buttonLabel = 'Playing ...';
    this.buttonDisabled = true;
  }
}
