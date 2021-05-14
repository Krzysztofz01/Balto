import { Component, OnInit } from '@angular/core';
import { Objective } from 'src/app/core/models/objective.model';
import { ObjectiveService } from 'src/app/core/services/objective.service';

@Component({
  selector: 'app-objectives',
  templateUrl: './objectives.component.html',
  styleUrls: ['./objectives.component.css']
})
export class ObjectivesComponent implements OnInit {
  private objectivesAll: number;
  private objectivesToday: number;
  private objectivesDeadline: number;
  public objectivesIncoming: Array<Objective>;

  constructor(private objectiveService: ObjectiveService) { }

  ngOnInit(): void {
    this.objectiveService.getAll(1).subscribe((res) => {
      this.objectivesAll = res.filter(o => o.daily == false).length;
      this.objectivesToday = res.filter(o => this.isToday(o.endingDate) == true).length;
      this.objectivesDeadline = res.filter(o => this.overDeadline(o.endingDate) == true).length;

    },
    (error) => {
      console.error(error);
    });
  }

  public today(): string {
    if(this.objectivesToday > 0) {
      return `You have ${ this.objectivesToday} tasks left to complete today!`;
    }
    return `You've completed all your tasks for today. Congratulations! 💪`;
  }

  public all(): string {
    if(this.objectivesAll > 0) {
      return `You have ${ this.objectivesAll } different tasks to complete in the future.`;
    }
    return `You have completed all your tasks! 👍`;
  }

  public deadline(): string {
    if(this.objectivesDeadline > 0) {
      return `Completion of ${ this.objectivesDeadline } different tasks exceeded! 😱`;
    }
    return `No deadlines were exceeded. 😄`;
  }

  private isToday(date: Date): boolean {
    const check = new Date(date);
    const today = new Date(Date.now());

    return check.getDate() == today.getDate() &&
      check.getMonth() == today.getMonth() &&
      check.getFullYear() == today.getFullYear();
  }

  private overDeadline(date: Date): boolean {
    const check = new Date(date);
    const today = new Date(Date.now());
    
    return check.getTime() < today.getTime();
  }

}
