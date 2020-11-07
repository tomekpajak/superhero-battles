import { ISuperHero } from './super-hero';

export interface ISuperHeroResponse {
    data: ISuperHero;
    succeeded: boolean;
    errors: string[]
    message: string;
}