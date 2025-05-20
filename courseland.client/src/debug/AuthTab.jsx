import React, { Component } from 'react';
import { API, add_param } from '../api.jsx';
import './AuthTab.scss';

class AuthTab extends Component {
    constructor(props) {
        super(props);
        this.state = {
            session: null,
            authForm: { email: '', password: '' }
        };
    }

    handleTakeRole = async (role) => {
        const account = role === 'manager' ? 'manager1' : 'root';
        console.debug(`Taking role ${role} with account ${account}`);

        const authData = role === "admin"
            ? { email: "testmail@yandex.ru", password: "qwerty12345" }
            : { email: "manager1@yandex.ru", password: "ytrewq54321" };

        // encrypt password
        try {
            const response = await fetch(API.login, {
                credentials: 'include',
                method: "POST",
                headers: {
                    'Content-Type': 'application/json;charset=utf-8'
                },
                body: JSON.stringify(authData)
            });

            if (!response.ok) {
                console.log(response);
                throw new Error(`Ошибка входа: ${response.statusText}`)
            }

            const user = await response.json();

            this.setState({
                session: user
            });
            window.location.reload();
        } catch (error) {
            console.log(error);
        }
    };

    handleLogin = async (e) => {
        e.preventDefault();
        const { authForm } = this.state;
        console.debug('Logging in with:', authForm);

        // encrypt password
        try {
            const response = await fetch(API.login, {
                credentials: 'include',
                method: "POST",
                headers: {
                    'Content-Type': 'application/json;charset=utf-8'
                },
                body: JSON.stringify(authForm)
            });

            if (!response.ok) {
                console.log(response);
                throw new Error(`Ошибка входа: ${response.statusText}`)
            }

            const user = await response.json();

            this.setState({
                session: user
            });
            window.location.reload();
        } catch (error) {
            console.log(error);
        }
    };

    handleResetSession = () => {
        fetch(API.logout, { credentials: 'include' })
            .then((_) => console.debug('Session reset'));

        this.setState({
            session: null
        });

        window.location.reload();
    };

    handleInputChange = (e) => {
        const { name, value } = e.target;
        this.setState(prevState => ({
            authForm: { ...prevState.authForm, [name]: value }
        }));
    };

    render() {
        const { session, authForm } = this.state;

        return (
            <div className="auth-tab">
                <div className="session-card card">
                    <h3>Current Session</h3>
                    <div className="session-info">
                        <div className="info-item">
                            <span className="label">Role:</span>
                            <span className="value">{session?.role || 'None'}</span>
                        </div>
                        <div className="info-item">
                            <span className="label">Email:</span>
                            <span className="value">{session?.email || 'None'}</span>
                        </div>
                    </div>
                </div>

                <div className="role-actions card">
                    <h3>Role Simulation</h3>
                    <div className="action-group">
                        <div className="action-item">
                            <button
                                className="btn role-btn"
                                onClick={() => this.handleTakeRole('manager')}
                            >
                                Take role "manager"
                            </button>
                            <span className="action-hint">Using account: <span className="highlight">manager1</span></span>
                        </div>
                        <div className="action-item">
                            <button
                                className="btn role-btn"
                                onClick={() => this.handleTakeRole('admin')}
                            >
                                Take role "admin"
                            </button>
                            <span className="action-hint">Using account: <span className="highlight">root</span></span>
                        </div>
                    </div>
                </div>

                <div className="login-form card">
                    <h3>Authentication</h3>
                    <form onSubmit={this.handleLogin}>
                        <div className="form-group">
                            <label>Email</label>
                            <input
                                type="email"
                                name="email"
                                value={authForm.email}
                                onChange={this.handleInputChange}
                                placeholder="Enter email"
                            />
                        </div>
                        <div className="form-group">
                            <label>Password</label>
                            <input
                                type="password"
                                name="password"
                                value={authForm.password}
                                onChange={this.handleInputChange}
                                placeholder="Enter password"
                            />
                        </div>
                        <button type="submit" className="btn primary">Login</button>
                    </form>
                </div>

                <button
                    className="btn danger reset-btn"
                    onClick={this.handleResetSession}
                >
                    Reset Session
                </button>
            </div>
        );
    }
}

export default AuthTab;