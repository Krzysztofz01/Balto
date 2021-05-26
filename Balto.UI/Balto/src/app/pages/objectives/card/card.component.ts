import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Objective } from 'src/app/core/models/objective.model';

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
    this.objectiveDeleteEvent.emit(this.objective);
  }

  public changeState(): void {
    this.objectiveStateEvent.emit(this.objective);
  }
}
