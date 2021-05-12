import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { Navlink } from './navlink.model';

const paths: Array<Navlink> = [
  { name: 'Dashboard', path: '', forLeader: false },
  { name: 'Projects', path: 'projects', forLeader: false },
  { name: 'Objectives', path: 'objectives', forLeader: false },
  { name: 'Notes', path: 'notes', forLeader: false },
  { name: 'Settings', path: 'settings', forLeader: false },
  { name: 'Leader', path: 'leader', forLeader: true },
];

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public navlinks: Array<Navlink>;
  public name: string;

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.name = this.authService.userValue.name;
    this.navlinks = new Array<Navlink>();

    if(this.isLeader()) {
      this.navlinks = paths;
    } else {
      this.navlinks = paths.filter(e => e.forLeader == false);
    }
  }

  public isLeader(): boolean {
    return (this.authService.userValue.role == 'Leader') ? true : false;
  }

  public logout(): void {
    this.authService.logout();
  }
}
