import React from 'react';
import Group from '../../js/group';
import '../../css/groupbar.css';
import url from 'url';
import ZeppelinSideBarComponent from '../zeppelin-bar/zeppelin-bar';
import classNames from 'classnames';

export interface GroupsSideBarProps {
    username: string;
    theme?: string;
}

export interface GroupsSideBarState {
    groups: Array<Group>;
    selectedGroup?: number;
}

export default class GroupsSideBarComponent extends React.Component<GroupsSideBarProps, GroupsSideBarState> {

    constructor(props: GroupsSideBarProps) {
        super(props);

        this.state = {
            groups: new Array<Group>()
        };
    }

    componentDidMount() {
        fetch(url.format({
            protocol: 'http',
            host: __API_URL__,
            pathname: `/api/groups/${this.props.username}`
        }))
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        groups: result
                    });
                }
            );
    }

    render() {
        return ([
            <div id='groups-bar' className={this.props.theme} key='groups-bar' >
                <div title='true'>
                    <span>Gruppi</span>
                </div>
                <hr className='horizontal-separator' />
                <div className='list'>
                    {this.renderGroups(this.state.groups)}
                </div>
            </div>,
            <ZeppelinSideBarComponent
                key='zeppelin-bar'
                groupId={this.state.selectedGroup}
                username={this.props.username}
                theme={this.props.theme} />
        ]);
    }

    renderGroups(props: Array<Group>) {
        if (!props) {
            return (
                <ul></ul>
            );
        }

        const listItems = props.map((g) => {
            const liClasses = classNames({
                'group-portrait': true,
                'selected': g.id === this.state.selectedGroup
            });

            return <li key={g.id.toString()} className={liClasses} onClick={(e) => this.selectGroup(e)} data-id={g.id.toString()} />;
        });

        return (
            <ul>{listItems}</ul>
        );
    }

    selectGroup(e: React.MouseEvent<HTMLLIElement>) {
        const obj = e.currentTarget.attributes.getNamedItem('data-id');
        const id = obj == null ? -1 : obj.value;

        this.setState(() => ({
            selectedGroup: +id
        }));
    }
}
