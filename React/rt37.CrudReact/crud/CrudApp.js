import React, { Component } from 'react'
import CrudInput from './CrudInput'
import CrudList from './CrudList'

class CrudApp extends React.Component {
    state = { 
        /* CrudInput 컴포넌트에서 사용 */           
        user:{
            id:0,
            name:'',
            power:0,
        },
        /* CrudList 컴포넌트에서 사용 */
        list : [
            {id:1, name: "슈퍼맨", power: 100},
            {id:2, name: "아쿠아맨", power: 300},
            {id:3, name: "스파이더맨", power: 500},
            {id:4, name: "배트맨", power: 30},
        ],
    }
    func = {
        insert(man){
            // man를 this.state.list 에 추가하기
            /* 
            max(id)를 찾는 방법 
            1. reduce() 메서드 사용.
                var maxObj = array.reduce( function(prev, next){
                    return prev.id > next.id ? prev:  next  // 최대값 id가 있는 객체 
                    return prev.id < next.id ? prev:  next  // 최소값 id가 있는 객체 
                })
                var newId  = maxObj.id + 1
            2. map()과 Math.max()를 사용하는 방법
                var arrIds = array.map( function(el){
                    return el.id
                })
                var newId  = Math.max(...arrIds) + 1
            */

            var maxObj = this.state.list.reduce( function(prev, next){
                return prev.id > next.id ? prev:  next 
            })
            //console.log( maxObj)

            if( maxObj )  {
                // man 객체 id 프러퍼티와 값 추가
                // man = {name:?, power: ? }
                man.id = maxObj.id + 1 // {id:?, name:?, power: ? }
            }
            else {
                man.id = 1
            }  
            //console.log( man ) // man = {id:man.id, name:man.name, power:man.power}
        
            // 신규 리스트 생성 :  push() 나 스프레드 연산자 활용해서
            const listCopy = [ ...this.state.list, man]
            this.setState({
                list: listCopy,
                user: { id:0, name:'', power:0 }
            })
        },
        doDel(id){
            //배열에서 삭제
            let r = window.confirm("정말로 삭제 하시겠습니까?")
            if( r ) {
                let mans = this.state.list.filter( (man,index)=>{
                    return man.id !== id
                })
                this.setState({
                    list: mans
                })
            }
        },
        doUp(id){
        
            //100씩 증가  
            let mans = this.state.list.filter( (man,index)=>{
                if( man.id === id ){
                    man.power = Number(man.power) + 100
                }
                return man
            })
            this.setState({
                list: mans
            })
        },
        doDown(id){
            //50씩 감소
            let mans = this.state.list.filter( (man,index)=>{
                if( man.id === id ){
                    man.power = Number(man.power) - 50
                }
                return man
            })
            this.setState({
                list: mans
            })
        },
        doEdit(paramMan){
            let mans = this.state.list.map( (man,index)=>{
                if( man.id === paramMan.id ){
                    return paramMan
                }
                else {
                    return man
                }
            })
            this.setState({
                list: mans
            })

        }
    }
    constructor(props){
        super()

        // this 바인드 
        this.func.insert = this.func.insert.bind(this)
        this.func.doUp   = this.func.doUp.bind(this)
        this.func.doDown = this.func.doDown.bind(this)
        this.func.doDel  = this.func.doDel.bind(this)
        this.func.doEdit = this.func.doEdit.bind(this)
    }
    render() {
        return (
            <div>
                <CrudInput {...this.state} {...this.func}>
                </CrudInput>
                <hr/>
                <CrudList {...this.state} {...this.func}>
                </CrudList>
            </div>
        )
    }
}

export default CrudApp;