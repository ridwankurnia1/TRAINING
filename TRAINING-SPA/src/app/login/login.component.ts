import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_service/auth.service';
import { UIService } from '../_service/ui.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = { };
  loginForm: FormGroup;
  returnUrl: string;
  loading = false;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private ui: UIService,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  login(): void {
    if (this.loginForm.invalid) {
      this.ui.validateFormEntry(this.loginForm);
      return;
    }
    this.loading = true;
    this.model = Object.assign({}, this.loginForm.getRawValue());
    this.authService.login(this.model).subscribe(
      () => {
        this.toastr.success('Login success');
        this.router.navigate([this.returnUrl]);
      }, (error) => {
        this.loading = false;
      });
  }
}
