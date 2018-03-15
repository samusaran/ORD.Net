import React from 'react';
import Group from '../../js/group'

export interface GroupsSideBarProps {
    groups: Array<Group>;
}

/*export interface GroupsSideBarState {
    groups: Array<Group>;
}*/

export class GroupsSideBarComponent extends React.Component<GroupsSideBarProps, {}> {

    constructor(props: GroupsSideBarProps) {
        super(props);
    }

    render() {
        return (
            <div id='groups-bar'>
                <div title='true'>
                    <span>Gruppi</span>
                </div>
                <hr className='horizontal-separator' />
                <div className='list'>
                    {this.renderGroups(this.props.groups)}
                </div>
            </div>
        );
    }

    renderGroups(props: Array<Group>) {
        if (!props) {
            return (
                <ul></ul>
            );
        }

        const listItems = props.map((g) =>
            <li key={g.id.toString()} className='group-portrait' />
        );

        return (
            <ul>{listItems}</ul>
        );
    }
}
