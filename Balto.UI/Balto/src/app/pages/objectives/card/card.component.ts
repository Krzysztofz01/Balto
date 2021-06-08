import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Objective } from 'src/app/core/models/objective.model';
import { DateParserService } from 'src/app/core/services/date-parser.service';
import { SoundService } from 'src/app/core/services/sound.service';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  @Input() objective: Objective;
  @Output() objectiveDeleteEvent = new EventEmitter<Objective>();
  @Output() objectiveStateEvent = new EventEmitter<Objective>();

  public status: boolean;

  constructor(private dateService: DateParserService, private soundService: SoundService) {}

  ngOnInit(): void {
    this.status = this.objective.finished;
  }

  public headerClass(): string {
    if(this.objective.finished) return 'card-header objective-finished';
    if(this.objective.daily) return 'card-header';
    if(new Date(this.objective.endingDate).getTime() < new Date(Date.now()).getTime()) return 'card-header objective-expired';
    return 'card-header';
  }

  public delete(): void {
    this.soundService.play('delete1');
    this.objectiveDeleteEvent.emit(this.objective);
  }

  public changeState(): void {
    this.soundService.play('notification1');
    this.objectiveStateEvent.emit(this.objective);
  }

  public deadlineColorClass(): string {
    const date = new Date(this.objective.endingDate).getTime();
    if(date < Date.now()) return 'deadline';
    if(date > Date.now() && date < Date.now() + (60 * 60 * 24 * 1000 * 3)) return 'deadline-warning';
    return '';
  }

  public date(date: Date): string {
    return this.dateService.parseDate(date);
  }
}
