import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TrelloIntegrationService {
  private readonly path: string = "integration/trello";
  private readonly server: string = environment.SERVER_URL;

  constructor(private httpClient: HttpClient) { }

  private preparePath(apiVersion: number): string {
    return `${ this.server }/api/v${ apiVersion }/${ this.path }`;
  }

  public migrate(file: File, apiVersion: number): Observable<void> {
    const formData = new FormData();
    formData.append("jsonFile", file);
    
    return this.httpClient.post<void>(this.preparePath(apiVersion), formData);
  }
}
