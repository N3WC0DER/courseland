import { Component } from 'react';
import Modal from './Modal.jsx';
import './Lists.scss';

export default class UsersList extends Component {

    constructor(props) {
        super(props);

        const users = [];

        this.state = {
            selected: null,
            users: users
        };
    }

    setSelectedUser = (user) => {
        
        this.setState((prevState) => ({
            selected: user,
            users: prevState.users
        }));
    }

    render() {
        const { selected, users } = this.state;
        const { searchQuery } = this.props;

        const filteredUsers = users.filter(user =>
            user.name.toLowerCase().includes(searchQuery.toLowerCase())
        );

        let usersList;

        if (filteredUsers.length == 0) {
            usersList = "Пусто";
        } else {
            usersList = filteredUsers.map(user => (
                <div key={user.id} className="user-item">
                    <span className="id">{user.id}</span>
                    <span className="name">{user.name}</span>
                    <span className="date">{user.date}</span>
                    <button onClick={this.setSelectedUser.bind(this, user)}>Изменить</button>
                </div>
            ));
        }

        return (
            <>
                <div id="users-list">
                    { usersList }
                    {selected && (
                        <Modal onClose={this.setSelectedUser.bind(this, null)}>
                            <h2>Редактирование пользователя</h2>
                            <p>Имя: {selected.name}</p>
                            <p>Дата регистрации: {selected.date}</p>
                            {/* Добавьте форму для редактирования */}
                        </Modal>
                    )}
                </div>
            </>
        );
    }
}