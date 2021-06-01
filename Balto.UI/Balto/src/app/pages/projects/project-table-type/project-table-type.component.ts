import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Project } from 'src/app/core/models/project.model';
import { ViewSettings } from '../view-settings.interface';

@Component({
  selector: 'app-project-table-type',
  templateUrl: './project-table-type.component.html',
  styleUrls: ['./project-table-type.component.css']
})
export class ProjectTableTypeComponent implements OnInit {
  @Input() viewSettings: ViewSettings;
  @Output() reloadEvent = new EventEmitter<Project>();

  constructor() { }

  ngOnInit(): void {
  }

}
