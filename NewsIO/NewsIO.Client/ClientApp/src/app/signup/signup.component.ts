import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../shared/services/user.service';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { FieldError } from '../shared/models/field-error.model';
import { Resp } from '../shared/models/resp.model';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signupForm: FormGroup;
  isUsernameError =false;
  usernameFieldError: string;
  isEmailError = false;
  emailFieldError: string;
  REGEX_EMAIL: string = '^[a-z0-9.]+\@[a-z0-9.-]+$';

  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router) {
  }

  ngOnInit() {
    this.initForm();
    this.userService.hide();
  }
  initForm() {
    this.signupForm = new FormGroup({
      'email': new FormControl('', Validators.pattern(this.REGEX_EMAIL)),
      'username': new FormControl('', Validators.required),
      'password': new FormControl('',Validators.minLength(8)),
      'firstName': new FormControl('', Validators.required),
      'lastName': new FormControl('', Validators.required),
      'location': new FormControl('', Validators.required)
    });
  }
  onSubmit() {
    this.userService.signup(this.signupForm.controls['email'].value, this.signupForm.controls['username'].value, this.signupForm.controls['password'].value,
                               this.signupForm.controls['firstName'].value,this.signupForm.controls['lastName'].value,this.signupForm.controls['location'].value)
      .subscribe(
      () => {
        this.resetErrors();
        if (this.userService.message === null) {
          this.router.navigateByUrl('/login')
        }
        else {
          if (this.userService.message == "Username already exist!") {
            this.isUsernameError = true;
            this.usernameFieldError = "Username already exists!";
          }
          else {
            this.isEmailError = true;
            this.emailFieldError = this.userService.message;
              }
        }
      },

      );
  }
 
  
private resetErrors() {
    this.isEmailError = false;
    this.emailFieldError ='';

    this.isUsernameError = false;
    this.usernameFieldError = '';
  }
}
