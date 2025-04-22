import { Component } from 'react';
import UsersList from './UsersList.jsx'
import RequestsList from './RequestsList.jsx'
import './Users.scss';

export default class Users extends Component {

    constructor(props) {
        super(props);

        this.state = {
            activeTab: "users",
            searchQuery: ''
        };
    }

    setSearchQuery = (e) => {
        this.setState((prevState) => ({
            activeTab: prevState.activeTab,
            searchQuery: e.target.value
        }));
    }

    setActiveTab = (tab) => {
        this.setState((prevState) => ({
            activeTab: tab,
            searchQuery: prevState.searchQuery
        }));
    }

    render() {
        const { activeTab, searchQuery } = this.state;

        return (
            <>
                <div id="users-content">
                    <div className="tabs">
                        <button
                            className={activeTab === 'users' ? 'active' : ''}
                            onClick={this.setActiveTab.bind(this, 'users')}
                        >
                            Пользователи
                        </button>
                        <button
                            className={activeTab === 'requests' ? 'active' : ''}
                            onClick={this.setActiveTab.bind(this, 'requests')}
                        >
                            Запросы
                        </button>
                        <input
                            type="text"
                            placeholder="Поиск..."
                            value={searchQuery}
                            onChange={this.setSearchQuery.bind(this)}
                        />
                    </div>
                    {activeTab === 'users' ? <UsersList searchQuery={searchQuery} /> : <RequestsList searchQuery={searchQuery} />}
                </div>
            </>
        );
    }
}