import React, { Component } from 'react';
import { API } from '../api.jsx';
import DataTable from './DataTable';
//import './UsersTab.scss';
class UsersTab extends Component {
    constructor(props) {
        super(props);
    }

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
    }
}

export default UsersTab;