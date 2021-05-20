import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { Project } from 'src/app/core/models/project.model';
import { ProjectService } from 'src/app/core/services/project.service';
import { AddModalComponent } from './add-modal/add-modal.component';
import { ProjectSyncService } from './project-sync/project-sync.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit, OnDestroy {
  public projects: Array<Project>;
  
  public selectedProject: Project;
  public selectProjectId: number;
  public syncSubscription: Subscription;

  constructor(private projectService: ProjectService, private projectSyncService: ProjectSyncService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.initializeProjects();

    this.syncSubscription = this.projectSyncService.project.subscribe(p => {
      this.selectedProject = p;
    });
  }

  ngOnDestroy(): void {
    this.projectSyncService.change(null);
    this.syncSubscription.unsubscribe();
  }

  private initializeProjects(afterAdd: boolean = false, subjectId: number = null): void {
    this.projects = new Array<Project>();
    this.projectService.getAllProjects(1).subscribe((res) => {
      this.projects = res;

      if(afterAdd) {
        this.projectSyncService.change(this.projects[this.projects.length - 1]);
      }

      if(subjectId != null) {
        const project = this.projects.find(p => p.id == subjectId);
        this.projectSyncService.change(project);
      }
    },
    (error) => {
      console.error(error);
    });
  }

  public selectProject(): void {
    if(this.selectProjectId != null) {
      const project = this.projects.find(p => p.id == this.selectProjectId);
      this.projectSyncService.change(project);
    }
  }

  public addNew(): void {
    const ref = this.modalService.open(AddModalComponent);

    ref.result.then((result) => {
      const project: Project = result;
      this.projectService.postOneProject(project, 1).subscribe((res) => {
        this.initializeProjects(true);
      },
      (error) => {
        console.error(error);
      })
    }, () => {});
  }

  public reload($event): void {
    const project: Project = $event;
    this.initializeProjects(false, project.id);
  }

}
