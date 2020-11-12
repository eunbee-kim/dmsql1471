
const app = new Vue({
    el: "#app",
    data: {
        editFriend: null,
        friends: [],
    },
    methods: {
        deleteF(id, i) {
            fetch("https://jsonplaceholder.typicode.com/users/" + id, {
                method: "DELETE",
            })
                .then(() => {
                    this.friends.splice(i, 1)
                })
        },

        updataFriend(friend) {
            fetch("https://jsonplaceholder.typicode.com/users/" + friend.id, {
                body: JSON.stringify(friend),
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
            })
                .then(() => {
                    this.editFriend = null;
                })
        }
    },
    mounted() {
        fetch("https://jsonplaceholder.typicode.com/users")
            .then(response => response.json())
            .then((data) => {
                this.friends = data;
                console.log(data);
            })
    },
    template: `
        <div>
            <li v-for="friend, i in friends">
                <div v-if="editFriend===friend.id">
                    <input v-on:keyup.13="updataFriend(friend) "v-model="friend.name"/>
                    <button v-on:click="updataFriend(friend)">save</button>
                </div>
                <div v-else>
                    <button v-on:click="editFriend=friend.id">edit</button>
                    <button v-on:click="deleteF(friend.id,i)">X</button>
                    {{friend.name}}
                </div>
            </li>
        </div>
    `,
});

