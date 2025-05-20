import React, { Component } from 'react';
import { FiChevronDown, FiChevronRight } from 'react-icons/fi';
import './DebugPage.scss';
import AuthTab from './AuthTab';
import UsersTab from './UsersTab';
import RolesTab from './RolesTab';

class DebugPage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            openTabs: {
                auth: false,
                users: false,
                roles: false
            }
        };
    }

    toggleTab = (tab) => {
        this.setState(prevState => ({
            openTabs: {
                ...prevState.openTabs,
                [tab]: !prevState.openTabs[tab]
            }
        }));
    };

    render() {
        const { openTabs } = this.state;

        return (
            <div className="debug-page">
                <h1>Backend Tester</h1>

                <div className="tabs-container">
                    <div className="tab-section">
                        <button
                            className={`tab-header ${openTabs.auth ? 'open' : ''}`}
                            onClick={() => this.toggleTab('auth')}
                        >
                            <span className="tab-title">Authentication</span>
                            <span className="tab-icon">
                                {openTabs.auth ? <FiChevronDown /> : <FiChevronRight />}
                            </span>
                        </button>
                        {openTabs.auth && (
                            <div className="tab-content">
                                <AuthTab />
                            </div>
                        )}
                    </div>

                    <div className="tab-section">
                        <button
                            className={`tab-header ${openTabs.users ? 'open' : ''}`}
                            onClick={() => this.toggleTab('users')}
                        >
                            <span className="tab-title">User Management</span>
                            <span className="tab-icon">
                                {openTabs.users ? <FiChevronDown /> : <FiChevronRight />}
                            </span>
                        </button>
                        {openTabs.users && (
                            <div className="tab-content">
                                <UsersTab />
                            </div>
                        )}
                    </div>

                    <div className="tab-section">
                        <button
                            className={`tab-header ${openTabs.roles ? 'open' : ''}`}
                            onClick={() => this.toggleTab('roles')}
                        >
                            <span className="tab-title">Role Management</span>
                            <span className="tab-icon">
                                {openTabs.roles ? <FiChevronDown /> : <FiChevronRight />}
                            </span>
                        </button>
                        {openTabs.roles && (
                            <div className="tab-content">
                                <RolesTab />
                            </div>
                        )}
                    </div>
                </div>
            </div>
        );
    }
}

export default DebugPage;