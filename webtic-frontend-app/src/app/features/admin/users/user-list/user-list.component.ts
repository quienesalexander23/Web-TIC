import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService, UsuarioDto } from '../../../../core/services/user.service';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {

  users: UsuarioDto[] = [];
  loading = false;
  totalItems = 0;

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.loading = true;
    this.userService.getUsers(1, 10).subscribe({
      next: (res) => {
        this.users = res.items;
        this.totalItems = res.totalItems;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching users', err);
        this.loading = false;
      }
    });
  }

  openUserModal() {
    console.log('Abrir modal de usuario');
  }

}
