<script setup lang="ts">
import { reactive, ref } from 'vue';
	type Player = {id:number}
	type Coords = {column:number,row:number}
	
	function GetAtCoords (row:number,column:number){
		return tiles.get({row:row,column:column});
	}
	
	function CheckIfWin(){
		
	}
	
	function OnTileClicked(rowarg: number, columnarg: number){
		console.log(`tile: ${rowarg}, ${columnarg}`)
		const coords = {row:rowarg,column:columnarg};
		const tile = tiles.get(coords)
		if(tile == undefined){
			tiles.set(coords,currentPlayer.value)
		}
	}
	
	
	const tiles = reactive(new Map<Coords,Player>());
	
	var p1:Player = reactive({id:0})
	var p2:Player = reactive({id:1})
	
	
	var currentPlayer = ref(p1);
	
	const p1Marked = []

	

</script>

<template>
	<div class="grid">
		<div class="row" v-for="row in 3">
			<div class="column" v-for="column in 3">
				<div class="gameTile pMarked" @click.left="OnTileClicked(row,column)">
					<div v-if="GetAtCoords(row,column)==p1">O</div>
					<div v-else-if="GetAtCoords(row,column)==p2">X</div>
					<div v-else class="nothingYet">m</div>
				</div>
			</div>
		</div>
	</div>
	<div>

	</div>
</template>

<style scoped>
.nothingYet{
	display:flex;
	border: 2px;
	border-style:solid;
	border-color: rgb(134, 134, 134);
	background-color: rgba(255, 255, 255, 0.95);
	align-items: center;
	justify-content: center;
	height:100%;
	width:100%;
}

.gameTile {
	display: flex;
	background-color: rgb(255, 255, 255);
	width:100%;
	height:100%;
}

.row {
	display: flex;
	flex-direction: row;
	min-height: 10vh;
	background-color: rgba(0, 0, 255, 0.103);
}

.column {
	display: flex;
	flex-direction: column;
	min-width: 10vh;
	justify-content: center;
	background-color: rgba(255, 0, 0, 0.11);
}
</style>