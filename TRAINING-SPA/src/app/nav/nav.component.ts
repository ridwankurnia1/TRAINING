import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  loading = false;
  constructor(
    private toastr: ToastrService,
    public authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  login(): void {
    this.loading = true;
    this.authService.login(this.model).subscribe(
      () => {
        this.toastr.success('Welcome ' + this.model.username);
        this.loading = false;
        this.model = {};
      }, (error) => {
        this.toastr.error('Unauthorized');
        this.loading = false;
      });
  }

  loggedIn(): boolean {
    return this.authService.loggedIn();
  }

  logout(): void {
    localStorage.removeItem('token');
    this.authService.decodedToken = null;
    this.toastr.info('Logged Out');
  }
}
