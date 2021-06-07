import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SoundService {
  private sounds: Array<string> = [
    '/assets/sound1.mp3',
    '/assets/sound2.mp3'
  ];

  constructor() { }

  public play(soundNumber: number): void {
    if(soundNumber > this.sounds.length - 1) return;
    const audio = new Audio();
    audio.src = this.sounds[soundNumber];
    audio.load();
    audio.play();
  }
}
