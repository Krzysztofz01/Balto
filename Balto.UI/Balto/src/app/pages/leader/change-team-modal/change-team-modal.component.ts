import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Team } from 'src/app/core/models/team.model';
import { TeamService } from 'src/app/core/services/team.service';

@Component({
  selector: 'app-change-team-modal',
  templateUrl: './change-team-modal.component.html',
  styleUrls: ['./change-team-modal.component.css']
})
export class ChangeTeamModalComponent implements OnInit {
  public teamForm: FormGroup;
  public teams: Array<Team>;

  public showNotification: boolean;
  public notificationContent: string;

  constructor(private modal: NgbActiveModal, private teamService: TeamService) { }

  ngOnInit(): void {
    this.showNotification = false;

    this.teams = new Array<Team>();
    this.teamService.getAll(1).subscribe((res) => {
      this.teams = res;
    },
    (error) => {
      console.error(error);
    });

    this.teamForm = new FormGroup({
      teamid: new FormControl(null, [ Validators.required ])
    });
  }

  public userTeamPatchSubmit() {
    if(this.teamForm.valid) {
      this.modal.close(this.teamForm.controls['teamid'].value);
    } else {
      this.showNotification = true;
      this.notificationContent = "Team data invalid. Check if all required fields are filled.";
    }

    this.teamForm.reset();
  }

}
