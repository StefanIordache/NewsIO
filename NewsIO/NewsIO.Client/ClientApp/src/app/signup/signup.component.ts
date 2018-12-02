import { Component, OnInit } from '@angular/core';
import { SignUpService } from './signup.service';
import { ActivatedRoute, Router } from "@angular/router";
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signupForm: FormGroup;

  constructor(private route: ActivatedRoute, private signupService: SignUpService, private router: Router) {
  }

  ngOnInit() {
    this.initForm();
  }
  initForm() {
    this.signupForm = new FormGroup({
      'username': new FormControl(''),
      'email': new FormControl(''),
      'password': new FormControl('')
    });
  }
  onSubmit() {
    this.signupService.signup(this.signupForm.controls['username'].value, this.signupForm.controls['email'].value, this.signupForm.controls['password'].value)
      .subscribe(
        () => {
          this.router.navigateByUrl('/login')
        }
      );
  }
}
