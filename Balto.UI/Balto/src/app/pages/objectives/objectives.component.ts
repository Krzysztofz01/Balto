import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Objective } from 'src/app/core/models/objective.model';
import { ObjectiveService } from 'src/app/core/services/objective.service';
import { AddModalComponent } from './add-modal/add-modal.component';

@Component({
  selector: 'app-objectives',
  templateUrl: './objectives.component.html',
  styleUrls: ['./objectives.component.css']
})
export class ObjectivesComponent implements OnInit {
  public objectives: Array<Objective>;
  public objectivesDaily: Array<Objective>;

  public showNotification: boolean;
  public notificationContent: string;
  public notificationType: string;

  constructor(private objectiveService: ObjectiveService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.initializeObjectives();
  }

  private initializeObjectives(): void {
    this.objectiveService.getAll(1)
      .subscribe(res => {
        this.objectives = new Array<Objective>();
        this.objectivesDaily = new Array<Objective>();

        res.forEach(o => {
          if(o.daily) {
            this.objectivesDaily.push(o);
          } else {
            this.objectives.push(o);
          }
        });

        this.objectives = this.sortByDate(this.objectives);
        this.objectivesDaily = this.sortByDate(this.objectivesDaily);
      },
      (error) => {
        console.error(error);
      });
  }

  public deleteEventReceiver($event): void {
    const objective: Objective = $event;

    this.objectiveService.deleteOne(objective.id, 1).subscribe(() => {
      this.notificationContent = 'Objective deleted!';
      this.notificationType = 'success';
      //this.showNotification = true;
      
      if(this.objectives.find(o => o.id == objective.id)) {
        this.objectives = this.objectives.filter(i => i.id !== objective.id);
        return;
      }
      
      if(this.objectivesDaily.find(o => o.id == objective.id)) {
        this.objectivesDaily = this.objectivesDaily.filter(i => i.id !== objective.id);
      }
    },
    (error) => {
      this.notificationContent = 'Objective delete error!';
      this.notificationType = 'danger';
      this.showNotification = true;

      console.error(error);
    });
  }

  public stateChangeEventReceiver($event): void {
    const objective: Objective = $event;

    this.objectiveService.changeState(objective.id, 1).subscribe(() => {
      this.notificationContent = 'Objective state changed!';
      this.notificationType = 'success';
      //this.showNotification = true;
      
      if(this.objectives.find(o => o.id == objective.id)) {
        this.objectives.find(i => i.id == objective.id).finished = !this.objectives.find(i => i.id == objective.id).finished;
        return;
      }

      if(this.objectivesDaily.find(o => o.id == objective.id)) {
        this.objectivesDaily.find(i => i.id == objective.id).finished = !this.objectivesDaily.find(i => i.id == objective.id).finished;
      }
    },
    (error) => {
      this.notificationContent = 'Objective state change error!';
      this.notificationType = 'danger';
      this.showNotification = true;

      console.error(error);
    });
  }

  private sortByDate(input: Array<Objective>): Array<Objective> {
    const safeTime = (date?: Date) => {
      return (date != null) ? new Date(date).getTime() : 0;
    };
    
    return input.sort((a, b) => {
      return safeTime(a.endingDate) - safeTime(b.endingDate)
    });
  }

  public addNew(): void {
    const ref = this.modalService.open(AddModalComponent);

    ref.result.then((result) => {
      if(result != null) {
        const objective: Objective = result;

        this.objectiveService.postOne(objective, 1).subscribe(() => {
          this.initializeObjectives();

          this.notificationContent = 'Objective added!';
          this.notificationType = 'success';
          this.showNotification = true;

        },
        (error) => {
          this.notificationContent = error;
          this.notificationType = 'danger';
          this.showNotification = true;
          
          console.error(error);
        });
      }
    }, () => {});
  }

}
