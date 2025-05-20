import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import Welcome from './Welcome.jsx'
import Dashboard from './dashboard/Dashboard.jsx'
import './main.scss';
import Users from './dashboard/users/Users.jsx';
import DebugPage from './debug/DebugPage.jsx';
import { features } from './config.js';

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <Router>
            <Routes>
                <Route path="/">
                    <Route index element={<Welcome />} />

                    <Route path="dashboard">
                        <Route path="home">
                            <Route index element={<Dashboard page="dashboard" />} />
                        </Route>
                        <Route path="users">
                            <Route index element={<Dashboard page={ <Users /> } />} />
                        </Route>
                        <Route path="stats">
                            <Route index element={<Dashboard page="stats" />} />
                        </Route>
                    </Route>

                    {features.debugTools && <Route path="debug">
                        <Route index element={<DebugPage />} />
                    </Route>}
                </Route>
                <Route path="*" element={<Navigate to="/" replace />} />
            </Routes>
        </Router>
    </StrictMode>,
)
