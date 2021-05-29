import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Team } from 'src/app/core/models/team.model';
import { User } from 'src/app/core/models/user.model';
import { TeamService } from 'src/app/core/services/team.service';
import { UserService } from 'src/app/core/services/user.service';
import { AddTeamModalComponent } from './add-team-modal/add-team-modal.component';
import { ChangeTeamModalComponent } from './change-team-modal/change-team-modal.component';

@Component({
  selector: 'app-leader',
  templateUrl: './leader.component.html',
  styleUrls: ['./leader.component.css']
})
export class LeaderComponent implements OnInit {
  public users: Array<User>;
  public teams: Array<Team>;

  constructor(private userService: UserService, private teamService: TeamService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.initializeData();
  }

  private initializeData(): void {
    this.users = new Array<User>();
    this.teams = new Array<Team>();

    this.userService.getAll(1).subscribe((res) => {
      this.users = res;
    },
    (error) => {
      console.error(error);
    });

    this.teamService.getAll(1).subscribe((res) => {
      this.teams = res;
    },
    (error) => {
      console.error(error);
    });
  }

  public userActivation(user: User): void {
    this.userService.changeActivation(user.id, 1).subscribe((res) => {
      this.users.find(u => u.id == user.id).isActivated = !this.users.find(u => u.id == user.id).isActivated;
    },
    (error) => {
      console.error(error);
    });
  }

  public userDelete(user: User): void {
    this.userService.deleteOne(user.id, 1).subscribe((res) => {
      this.users.splice(this.users.indexOf(user), 1);
    },
    (error) => {
      console.error(error);
    });
  }

  public userChangeTeam(user: User): void {
    const ref = this.modalService.open(ChangeTeamModalComponent);

    ref.result.then((result) => {
      const teamId = result as number;
      this.userService.setTeam(user.id, teamId, 1).subscribe((res) => {
        this.initializeData();
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public addTeam(): void {
    const ref = this.modalService.open(AddTeamModalComponent);

    ref.result.then((result) => {
      const team = result as Team;
      this.teamService.postOne(team, 1).subscribe((res) => {
        this.initializeData();
      },
      (error) => {
        console.error(error);
      });
    }, () => {});
  }

  public deleteTeam(team: Team): void {
    this.teamService.deleteOne(team.id, 1).subscribe((res) => {
      this.initializeData();
    },
    (error) => {
      console.error(error);
    });
  }

  public parseUserName(user: User): string {
    if(user.team != null) {
      return `${user.name} (${user.email}) - ${user.team.name}`;
    }
    return `${user.name} (${user.email})`;
  }

}
