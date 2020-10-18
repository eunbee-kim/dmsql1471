import React, { Component } from 'react'
import CrudListItem from './CrudListItem'

class CrudList extends React.Component {
    render() {
        const rows = this.props.list // == CrudApp.state.list
        const trs = rows.map( (item,index)=>{
            /* item = {id: 1, name:"슈퍼맨", power: 100 }, */
            return (
                <CrudListItem 
                    key={item.id} 
                    man={item} 
                    {...this.props} 
                >
                </CrudListItem>
            )
        })
        
        return (
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>NAME</th>
                        <th>POWER</th>
                        <th>CRUD</th>
                    </tr>
                </thead>
                <tbody>
                    {trs}
                </tbody>
            </table>
        )
    }
}

export default CrudList;