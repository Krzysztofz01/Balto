import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class SoundService {
  private readonly localStorageKey = 'GLOBAL_SOUND_SETTINGS';

  constructor(private localStorage: LocalStorageService) {}

  public play(soundName: string): void {
    if(this.checkSettings()) {
      switch(soundName) {
        case 'notification1': this.playSound(1); break;
        case 'notification2': this.playSound(2); break;
        case 'checked1': this.playSound(3); break;
        case 'delete1': this.playSound(4); break;
        default: console.error('No sound found!'); break;
      }
    }
  }

  private playSound(soundNumber: number) : void {
    const audio = new Audio();
    audio.src = `/assets/sound${ soundNumber }.mp3`;
    audio.load();
    audio.play();
  }

  private checkSettings(): boolean {
    const settings = this.localStorage.get(this.localStorageKey);
    if(settings == null) return true;
    return settings.status;
  }
}
