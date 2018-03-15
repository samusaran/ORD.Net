import * as React from 'react'
import { GroupsSideBarComponent, GroupsSideBarProps } from './groupbar/groupbar'
import { ZeppelinSideBarComponent } from './zeppelin-bar/zeppelin-bar'
import { MainGridComponent } from './main-grid/main-grid'
import { TitleBar } from './title-bar/title-bar'
import Group from '../js/group'
import url from 'url'
import username from 'username'

interface AppState {
    username: string,
    groupbarProps: GroupsSideBarProps
}

export class App extends React.Component<{}, AppState> {

    constructor(props: any) {
        super(props);

        this.state = {
            username: username.sync(),
            groupbarProps: {
                groups: new Array<Group>()
            }
        }
    }

    public render() {
        const className = '';

        return (
            <div id='desktop-app-wrapper' className={className}>
                {this.renderTitleBar()}
                {this.renderApp()}
                {this.renderStyle()}
            </div>
        );
    }

    componentDidMount() {
        fetch(url.format({
            protocol: 'http',
            host: __API_URL__,
            pathname: `/api/groups/${this.state.username}`
        }))
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        groupbarProps: {
                            groups: result
                        }
                    })
                }
            )
    }

    private renderApp() {
        return (
            <div id='desktop-app-content'>
                {this.renderGroups()}
                {this.renderZeppelins()}
                {this.renderGrid()}
            </div>
        )
    }

    private renderTitleBar() {
        return (
            <TitleBar title='ORD.Net UI' disableMaximize={false} disableMinimize={false} />
        )
    }

    private renderGroups() {
        return (
            <GroupsSideBarComponent groups={this.state.groupbarProps.groups} />
        )
    }

    private renderZeppelins() {
        return (
            <ZeppelinSideBarComponent />
        )
    }

    private renderGrid() {
        return (
            <MainGridComponent />
        )
    }

    private renderStyle() {
        return (
            null // implementare ThemeManager
        )
    }
}
