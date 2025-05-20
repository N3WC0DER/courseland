import React, { Component } from 'react';
import { API } from '../api.jsx';
import DataTable from './DataTable';
//import './UsersTab.scss';
class UsersTab extends Component {
    constructor(props) {
        super(props);
        /*this.state = {
            users: [],
            editingUserId: null
        };*/
    }

    /*componentDidMount() {
        this.fetchUsers();
    }

    fetchUsers = () => {
        // Mock data - replace with API call
        const mockUsers = [
            { id: 1, name: 'User1', email: 'user1@example.com', passwordHash: 'hash1', role: 'user', registeredAt: '2023-01-01' },
            { id: 2, name: 'User2', email: 'user2@example.com', passwordHash: 'hash2', role: 'manager', registeredAt: '2023-01-02' },
        ];
        this.setState({ users: mockUsers });
    };

    handleAddUser = () => {
        const { users } = this.state;

        // cannot add more than 1 new user
        if (users.findIndex(item => item.isNew) !== -1) return;

        const newUser = {
            id: this.state.users.length + 1,
            name: '',
            email: '',
            passwordHash: '',
            role: 'user',
            registeredAt: new Date().toISOString().split("T")[0],
            isNew: true
        };
        this.setState(prevState => ({
            users: [...prevState.users, newUser],
            editingUserId: newUser.id
        }));
    };

    handleSaveUser = (userId) => {
        if (this.anyEmptyFields(userId)) return;

        // API call here
        this.changeFocusEditing(null);
        this.fetchUsers(); // Refresh data
    };

    handleDeleteUser = (userId) => {
        // API call here
        this.fetchUsers(); // Refresh data
    };

    handleInputChange = (userId, field, value) => {
        this.setState(prevState => ({
            users: prevState.users.map(user =>
                user.id === userId ? { ...user, [field]: value } : user
            )
        }));
    };

    changeFocusEditing = (to) => {
        let { users, editingUserId } = this.state;

        if (editingUserId === to) return;

        if (editingUserId === null || this.isNew(to)) {
            this.setState({ editingUserId: to });
            return;
        }

        if (this.isNew(editingUserId) && !this.isNew(to)) {
            users = users.filter((value) => !value.isNew);
            this.setState({ users: users, editingUserId: to })
        }

        this.setState({ editingUserId: to });
    }

    isNew = (userId) => {
        const { users } = this.state;
        const user = users.find((item) => item.id === userId);
        return user?.isNew ?? false;
    }

    anyEmptyFields = (userId) => {
        const { users } = this.state;
        const user = users.find((item) => item.id === userId);
        if (user.name.trim() === "" ||
            user.email.trim() === "" ||
            user.passwordHash.trim() === "" ||
            user.role.trim() === "")
            return true;
        else
            return false;
    }*/

    render() {

        const headers = ["ID", "Name", "Email", "Password Hash", "Role", "Registered At (UTC)"];
        const defaultRow = { id: 0, name: "", email: "", passwordHash: "", role: "", registeredAt: new Date().toISOString() }

        const prepareData = (data) => {
            return data.map(user => ({ ...user, role: user.role.name, registeredAt: user.registeredAt + "Z" }));
        }

        return (
            <div className="users-tab">
                <DataTable request={API.users} headers={headers} defaultRow={defaultRow} tableName="User" prepareData={prepareData} />
            </div>
        );

        /*const { users, editingUserId } = this.state;

        return (
            <div className="users-tab">
                <div className="table-header">
                    <h3>User Management</h3>
                    <button
                        className="btn primary"
                        onClick={this.handleAddUser}
                    >
                        <FiPlus /> Add User
                    </button>
                </div>

                <div className="table-container">
                    <table className="data-table">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Password Hash</th>
                                <th>Role</th>
                                <th>Registered (UTC)</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users.map(user => (
                                <tr key={user.id} className={editingUserId === user.id ? 'editing' : ''}>
                                    <td>{user.id}</td>
                                    <td>
                                        {editingUserId === user.id ? (
                                            <input
                                                className={user.name.trim() === '' ? 'empty-required' : ''}
                                                type="text"
                                                value={user.name}
                                                onChange={(e) => this.handleInputChange(user.id, 'name', e.target.value)}
                                            />
                                        ) : (
                                            user.name
                                        )}
                                    </td>
                                    <td>
                                        {editingUserId === user.id ? (
                                            <input
                                                className={user.email.trim() === '' ? 'empty-required' : ''}
                                                type="email"
                                                value={user.email}
                                                onChange={(e) => this.handleInputChange(user.id, 'email', e.target.value)}
                                            />
                                        ) : (
                                            user.email
                                        )}
                                    </td>
                                    <td>
                                        {editingUserId === user.id ? (
                                            <input
                                                className={user.passwordHash.trim() === '' ? 'empty-required' : ''}
                                                type="text"
                                                value={user.passwordHash}
                                                onChange={(e) => this.handleInputChange(user.id, 'passwordHash', e.target.value)}
                                            />
                                        ) : (
                                            user.passwordHash
                                        )}
                                    </td>
                                    <td>
                                        {editingUserId === user.id ? (
                                            <input
                                                className={user.role.trim() === '' ? 'empty-required' : ''}
                                                type="text"
                                                value={user.role}
                                                onChange={(e) => this.handleInputChange(user.id, 'role', e.target.value)}
                                            />
                                        ) : (
                                            user.role
                                        )}
                                    </td>
                                    <td>
                                        {user.registeredAt}
                                    </td>
                                    <td className="actions-cell">
                                        {editingUserId === user.id ? (
                                            <div className="actions">
                                                <button className="btn-icon success" onClick={() => this.handleSaveUser(user.id)}>
                                                    <FiCheck />
                                                </button>
                                                <button className="btn-icon failure" onClick={() => this.changeFocusEditing(null)}>
                                                    <FiX />
                                                </button>
                                            </div>
                                        ) : (
                                            <div className="actions">
                                                <button onClick={() => this.changeFocusEditing(user.id)}>
                                                    <FiEdit2 />
                                                </button>
                                                <button onClick={() => this.handleDeleteUser(user.id)}>
                                                    <FiTrash2 />
                                                </button>
                                            </div>
                                        )}
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        );*/
    }
}

export default UsersTab;