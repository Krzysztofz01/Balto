import { Component, OnInit } from '@angular/core';
import { Objective } from 'src/app/core/models/objective.model';
import { ObjectiveService } from 'src/app/core/services/objective.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor() { }

  //FIX: https://stackoverflow.com/questions/34364880/expression-has-changed-after-it-was-checked

  ngOnInit(): void {
  }

}
