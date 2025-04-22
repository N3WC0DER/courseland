import { Component } from 'react';
import Sidebar from './Sidebar';
import './Dashboard.scss';

export default class Dashboard extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        const { page } = this.props;

        return (
            <>
                <div id="dashboard">
                    <Sidebar />
                    <div id="page">{page}</div>
                </div>
            </>
        );
    }
}