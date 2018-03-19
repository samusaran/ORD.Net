import * as React from 'react';
import GroupsSideBarComponent from './groupbar/groupbar';
import { TitleBar } from './title-bar/title-bar';
import username from 'username';

interface AppState {
    username: string;
    theme: 'theme-default' | 'theme-light';
}

export class App extends React.Component<{}, AppState> {

    constructor(props: any) {
        super(props);

        this.state = {
            username: username.sync(),
            theme: 'theme-default'
        };
    }

    public render() {
        const className = '';

        return (
            <div id='desktop-app-wrapper' className={className}>
                {this.renderTitleBar()}
                {this.renderApp()}
            </div>
        );
    }

    private renderApp() {
        return (
            <div id='desktop-app-content'>
                {this.renderGroups()}
            </div>
        );
    }

    private renderTitleBar() {
        return (
            <TitleBar
                title='ORD.Net UI'
                disableMaximize={false}
                disableMinimize={false} />
        );
    }

    private renderGroups() {
        return (
            <GroupsSideBarComponent
                username={this.state.username}
                theme={this.state.theme} />
        );
    }
}
