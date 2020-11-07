import { PlayerType } from '../../enums/player-type.enum';
import { IPlayer } from '../../interfaces/player';

export class Player implements IPlayer{
    constructor(public name: string, public type: PlayerType) 
    {}
}
