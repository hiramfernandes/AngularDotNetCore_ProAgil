import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService} from 'ngx-toastr';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerForm: FormGroup;
  user: User;

  constructor(
      public fb: FormBuilder,
      public router: Router,
      private toastr: ToastrService,
      private authService: AuthService
      ) { }

  ngOnInit() {
    this.validation();
  }

  validation() {
    this.registerForm = this.fb.group({
      fullName : ['', Validators.required],
      email : ['', [Validators.required, Validators.email]],
      userName : ['', Validators.required],
      passwords: this.fb.group({
        password : ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword : ['', Validators.required]
      }, {validator : this.comparaSenhas} )
    });
  }

  comparaSenhas(fb: FormGroup) {
    const confirmaSenhaCtrl = fb.get('confirmPassword');
    if (confirmaSenhaCtrl.errors == null || 'mismatch' in confirmaSenhaCtrl.errors) {
      if (fb.get('password').value !== confirmaSenhaCtrl.value) {
        confirmaSenhaCtrl.setErrors({ mismatch : true });
      } else {
        confirmaSenhaCtrl.setErrors = null;
      }
    }
  }

  cadastrarUsuario() {
    if (this.registerForm.valid) {
      this.user = Object.assign(
        {password: this.registerForm.get('passwords.password').value},
        this.registerForm.value);

      this.authService.register(this.user).subscribe(
        () => {
          this.router.navigate(['/user/login']);
          this.toastr.success('Cadastrado com sucesso');
        }, error => {
        console.log(error);
        this.toastr.error(error);
      });
    }
  }
}
