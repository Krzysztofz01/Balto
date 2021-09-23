import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/authentication/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public name: string;

  private readonly navlinks = [
    { name: 'Dashboard', path: '', forLeader: false },
    { name: 'Projects', path: 'projects', forLeader: false },
    { name: 'Notes', path: 'notes', forLeader: false },
    { name: 'Objectives', path: 'objectives', forLeader: false }
  ];

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.name = this.authService.currentUserClaims.email;
  }

  public getNavlinks(): Array<any> {
    if(this.authService.currentUserClaims.role == 'Leader') {
      return this.navlinks;
    } else {
      return this.navlinks.filter(l => !l.forLeader);
    }
  }

  public logout(): void {
    this.authService.logout();
  }
}
