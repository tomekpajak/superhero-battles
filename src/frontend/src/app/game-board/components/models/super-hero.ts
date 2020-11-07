import { ISuperHero } from '../../interfaces/super-hero';

export class SuperHero implements ISuperHero{
    constructor(public name: string, public attack: number, public defence: number)
    {}
}
