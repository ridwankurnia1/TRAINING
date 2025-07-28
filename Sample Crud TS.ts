// app.ts

// Define the AngularJS module and controller
angular.module('crudApp', [])
    .controller('CrudController', function() {
        var ctrl = this;

        // Sample initial data (could be fetched from an API)
        ctrl.users = [
            { id: 1, name: 'John Doe', email: 'john@example.com' },
            { id: 2, name: 'Jane Doe', email: 'jane@example.com' }
        ];

        // User model
        ctrl.user = { id: null, name: '', email: '' };

        // Save a new user or update an existing one
        ctrl.saveData = function() {
            if (ctrl.user.id) {
                // Update the existing user
                var index = ctrl.users.findIndex(u => u.id === ctrl.user.id);
                if (index !== -1) {
                    ctrl.users[index] = angular.copy(ctrl.user);
                }
            } else {
                // Add a new user
                ctrl.user.id = Date.now(); // simple id generation using current timestamp
                ctrl.users.push(angular.copy(ctrl.user));
            }

            // Reset the form
            ctrl.resetForm();
        };

        // Edit user
        ctrl.editUser = function(user) {
            ctrl.user = angular.copy(user);
        };

        // Delete a user
        ctrl.deleteUser = function(id) {
            var index = ctrl.users.findIndex(u => u.id === id);
            if (index !== -1) {
                ctrl.users.splice(index, 1);
            }
        };

        // Reset form
        ctrl.resetForm = function() {
            ctrl.user = { id: null, name: '', email: '' };
        };
    });
