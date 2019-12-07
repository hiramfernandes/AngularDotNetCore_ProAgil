import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  baseUrl = 'http://localhost:5000/api/eventos';

  constructor(private http: HttpClient) { }

  getAllEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseUrl);
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseUrl}/${id}`);
  }

  getEventoByTema(tema: string): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseUrl}/getTema/${tema}`);
  }

  postEvento(evento: Evento) {
    return this.http.post(this.baseUrl, evento);
  }

  putEvento(evento: Evento) {
    return this.http.put(this.baseUrl, evento);
  }

  deleteEvento(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

}
