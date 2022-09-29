import { gql } from 'apollo-angular'

const GET_PART_NUMBER = gql`
  query GetPartNumber ($customerId: String!, $partNumber: String) {
    partNumber (customerId: $customerId, partNumber: $partNumber) {
        modelName
        partNumber
        partDescription
        status
        createdBy
        onHandStock
    }
  }
`

const GET_LIST_PART_NUMBER = gql`
  query GetListPartNumber ($skip: Int, $take: Int, $filter: PartNumberTypeFilterInput) {
    listPartNumber (
      skip: $skip
      take: $take
      where: $filter
    ) {
      items {
        modelName
        partNumber
        partDescription
        customerId
        status
        eoNumber
        alcNumber
      }
      totalCount
    }
  }
`


// const ADD_TODO = gql`
//   mutation addTodo($name: String!, $description: String!) {
//     addTodo(name: $name, description: $description) {
//       id
//       name
//       description
//     }
//   }
// `

// const DELETE_TODO = gql`
//   mutation deleteTodo($id: Int!) {
//     deleteTodo(id: $id) {
//       id
//     }
//   }
//   `

export { GET_PART_NUMBER, GET_LIST_PART_NUMBER }