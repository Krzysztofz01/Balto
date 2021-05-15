import { Component, OnInit } from '@angular/core';
import { Objective } from 'src/app/core/models/objective.model';
import { ObjectiveService } from 'src/app/core/services/objective.service';

@Component({
  selector: 'app-objective-widget',
  templateUrl: './objective-widget.component.html',
  styleUrls: ['./objective-widget.component.css']
})
export class ObjectiveWidgetComponent implements OnInit {
  private objectives: Array<Objective>;

  public allStatus: boolean;
  public todayStatus: boolean;
  public deadlineStatus: boolean;

  constructor(private objectivesService: ObjectiveService) { }

  ngOnInit(): void {
    this.objectives = new Array<Objective>();

    this.deadlineStatus = false;
    this.objectivesService.getAll(1).subscribe((res) => {
      this.objectives = res;
    });
  }

  public allCount(): string {
    const all = this.objectives.filter(o => o.daily == false).length;
    const finished = this.objectives.filter(o => o.daily == false && o.finished == true).length;
    return `You have completed ${ finished } out of ${ all } of your tasks!`
  }

  public deadline(): string {
    const deadline = this.objectives.filter(o => this.overDeadline(o.endingDate) == true && o.finished == false && o.daily == false).length;
    if(deadline > 0) this.deadlineStatus = true;

    return `You have ${ deadline } tasks in which you exceeded the deadline!`;
  }

  public today(): string {
    const today = this.objectives.filter(o => this.isToday(o.endingDate) == true).length;
    const todayFinished = this.objectives.filter(o => this.isToday(o.endingDate) == true && o.finished == true).length;

    return `You have completed ${ todayFinished } out of ${ today } tasks planned for today!`;
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
