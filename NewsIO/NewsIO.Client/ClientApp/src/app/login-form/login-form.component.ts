import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { ActivatedRoute, Router } from "@angular/router";
import { FormGroup,FormControl } from '@angular/forms';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private route: ActivatedRoute, private loginService: LoginService, private router: Router) {
   }

  ngOnInit() {
    this.initForm();
  }
  initForm() {
    this.loginForm = new FormGroup({
      'email': new FormControl(''),
      'password': new FormControl('')
    });
  }
  onSubmit() {
    this.loginService.login(this.loginForm.controls['email'].value, this.loginForm.controls['password'].value )
      .subscribe(
        () => {
          this.router.navigateByUrl('/')
        }
      );
  }

}
