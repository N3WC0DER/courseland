import { Component } from 'react';
import { NavLink } from "react-router";
import { FaHome, FaUsers, FaChartLine } from 'react-icons/fa';
import './Sidebar.scss';

export default class Sidebar extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <>
                <div id="sidebar">
                    <nav>
                        <NavLink to="/dashboard/home">
                            <FaHome />
                            <span>Домашняя страница</span>
                        </NavLink>
                        <NavLink to="/dashboard/users">
                            <FaUsers />
                            <span>Пользователи</span>
                        </NavLink>
                        <NavLink to="/dashboard/stats">
                            <FaChartLine />
                            <span>Статистика</span>
                        </NavLink>
                    </nav>
                </div>
            </>
        );
    }
}