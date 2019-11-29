import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any;
  //  = [
  //   {
  //     eventoId: 1,
  //     local: 'São Paulo', 
  //     dataEvento: '1/1/2020', tema: 'Angular', qtdPessoas: 250, lote: '2'
  //   },
  //   {
  //     eventoId: 2,
  //     local: 'Belo Horizonte',
  //     dataEvento: '25/12/2019',
  //     tema: '.Net Core',
  //     qtdPessoas: 150, lote: '1'
  //   },
  //   {
  //     eventoId: 3,
  //     local: 'Rio de Janeiro',
  //     dataEvento: '4/4/2020',
  //     tema: '.Net Framework',
  //     qtdPessoas: 14, lote: '1'
  //   }
  // ];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.eventos = this.http.get('http://localhost:5000/api/values').subscribe(
      response => {
        this.eventos = response;
      }, error => {
        console.log(error);
      });
  }
}
