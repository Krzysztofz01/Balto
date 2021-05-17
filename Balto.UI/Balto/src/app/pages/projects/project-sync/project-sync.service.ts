import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Project } from 'src/app/core/models/project.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectSyncService {
  private projectSubject = new BehaviorSubject<Project>(null);
  public project = this.projectSubject.asObservable();

  constructor() { }

  public change(project: Project): void {
    this.projectSubject.next(project);
  }
}
